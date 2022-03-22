using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ExamMVVM
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void PropertyChanging(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public static AddStudent addStudent;
        public static UpdateStudent updateStudent;
        public static UpdateTeacher updateTeacher;
        public static AddTeacher addTeacher;
        UniversityContext universityContext;
        public static Student selectedItemSt;
        public static Teacher selectedItemTeach; 
        List<Student> students;
        List<Teacher> teachers;
        string findText;
        public ViewModel()
        {            
            students = new List<Student>();
            universityContext = new UniversityContext();
            teachers = new List<Teacher>();
            RefreshDataStudent();
            RefreshDataTeacher();
        }
        public Student SelectedItemSt
        {
            get { return selectedItemSt; }
            set
            {
                selectedItemSt = value;
                PropertyChanging("SelectedItemSt");
            }
        }
        public Teacher SelectedItemTeach
        {
            get { return selectedItemTeach; }
            set
            {
                selectedItemTeach = value;
                PropertyChanging("SelectedItemTeach");
            }
        }
        public ICommand AddButtonSt
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  addStudent = new AddStudent();
                  addStudent.ShowDialog();
                  RefreshDataStudent();
              }
              );
            }
        }
        public ICommand AddButtonTeacher
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  addTeacher = new AddTeacher();
                  addTeacher.ShowDialog();
                  RefreshDataTeacher();
              }
              );
            }
        }
        public ICommand UpdateButtonSt
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  if (selectedItemSt != null) 
                  {
                      updateStudent = new UpdateStudent();
                      updateStudent.ShowDialog();
                      RefreshDataStudent();
                  }                      
              }
              );
            }
        }
        public ICommand UpdateButtonTeacher
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  if (selectedItemTeach != null) 
                  {
                      updateTeacher = new UpdateTeacher();
                      updateTeacher.ShowDialog();
                      RefreshDataTeacher();
                  }
              }
              );
            }
        }
        public ICommand DeleteButtonSt
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  if (selectedItemSt != null)
                  {
                      MessageBoxResult result =
                       MessageBox.Show("Действительно удалить студента "
                           + selectedItemSt.Surname + "?", "Удалить?", MessageBoxButton.YesNo,
                           MessageBoxImage.Question);

                      if (result == MessageBoxResult.Yes)
                      {
                          universityContext.Students.Remove(selectedItemSt);
                          universityContext.SaveChanges();
                          RefreshDataStudent();
                      }
                  }
              });
            }
        }
        public ICommand DeleteButtonTeacher
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  if (selectedItemTeach != null)
                  {
                      MessageBoxResult result =
                       MessageBox.Show("Действительно удалить студента "
                           + selectedItemTeach.Surname + "?", "Удалить?", MessageBoxButton.YesNo,
                           MessageBoxImage.Question);

                      if (result == MessageBoxResult.Yes)
                      {
                          universityContext.Teachers.Remove(selectedItemTeach);
                          universityContext.SaveChanges();
                          RefreshDataTeacher();
                      }
                  }
              });
            }
        }
        public string FindStudent
        {
            get { return findText; }
            set
            {
                findText = value;
                FindStudents(findText);
                PropertyChanging("FindText");
            }
        }
        public string FindTeacher
        {
            get { return findText; }
            set
            {
                findText = value;
                FindTeachers(findText);
                PropertyChanging("FindText");
            }
        }
        public string ShowStudent
        {
            get { return findText; }
            set
            {
                findText = value;
                ShowStudents(findText);
                PropertyChanging("FindText");
            }
        }
        void FindStudents(string text = "")
        {
            students = (from student in universityContext.Students
                        where student.Surname.Contains(text)
                        select student).ToList();
            Students = students;
        }
        void FindTeachers(string text = "")
        {
            teachers = (from teacher in universityContext.Teachers
                        where teacher.Surname.Contains(text)
                        select teacher).ToList();
            Teachers = teachers;
        }
        void ShowStudents(string text = "")
        {
            students = (from student in universityContext.Students
                        where student.TeacherId.ToString().Contains(text)
                        select student).ToList();
            Students = students;
        }
        void RefreshDataStudent()
        {
            students = (from stud in universityContext.Students
                        where true
                        select stud).ToList();
            Students = students;
        }
        void RefreshDataTeacher()
        {
            teachers = (from teacher in universityContext.Teachers
                        where true
                        select teacher).ToList();
            Teachers = teachers;
        }
        public List<Student> Students
        {
            get { return students; }
            set
            {
                students = new List<Student>(students);
                PropertyChanging("Students");
            }
        }
        public List<Teacher> Teachers
        {
            get { return teachers; }
            set
            {
                teachers = new List<Teacher>(teachers);
                PropertyChanging("Teachers");
            }
        }
    }
}
