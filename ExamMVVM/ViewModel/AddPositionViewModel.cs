using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ExamMVVM
{
    class AddPositionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void PropertyChanging(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        UniversityContext universityContext;
        string discipline;
        string surname;
        string surnameT;
        string group;
        double aveScore;
        public static int id;

        public AddPositionViewModel()
        {
            if (ViewModel.selectedItemSt != null)
            {
                Surname = ViewModel.selectedItemSt.Surname;
                Group = ViewModel.selectedItemSt.Group;
                AveScore = ViewModel.selectedItemSt.AverageScore;
                Id = ViewModel.selectedItemSt.TeacherId;
            }
            if (ViewModel.selectedItemTeach != null)
            {
                SurnameT = ViewModel.selectedItemTeach.Surname;
                Discipline = ViewModel.selectedItemTeach.Discipline;
            }
            universityContext = new UniversityContext();
        }

        public string Discipline
        {
            get { return discipline; }
            set
            {
                discipline = value;
                PropertyChanging("Discipline");
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                PropertyChanging("Id");
            }
        }
        public double AveScore
        {
            get { return aveScore; }
            set
            {
                aveScore = value;
                PropertyChanging("AveScore");
            }
        }
        public string Group
        {
            get { return group; }
            set
            {
                group = value;
                PropertyChanging("Group");
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                PropertyChanging("Surname");
            }
        }
        public string SurnameT
        {
            get { return surnameT; }
            set
            {
                surnameT = value;
                PropertyChanging("SurnameT");
            }
        }
        public ICommand AddButtonStudent
        {
            get
            {
                return new ButtonsCommand(
                    () =>
                    {
                        universityContext.Students.Add(new Student()
                        { Surname = surname, Group = group, AverageScore = aveScore, TeacherId = id });
                        universityContext.SaveChanges();
                        ViewModel.addStudent.Close();
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
                        universityContext.Teachers.Add(new Teacher()
                        { Surname = SurnameT, Discipline = discipline });
                        universityContext.SaveChanges();
                        ViewModel.addTeacher.Close();
                    }
                    );
            }
        }
        public ICommand UpdateButtonStudent
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  Student update = universityContext.Students.Find(ViewModel.selectedItemSt.Id);
                  update.Surname = surname;
                  update.Group = group;
                  update.AverageScore = aveScore;
                  update.TeacherId = id;
                  universityContext.SaveChanges();
                  ViewModel.updateStudent.Close();
              });
            }
        }
        public ICommand UpdateButtonTeacher
        {
            get
            {
                return new ButtonsCommand(
              () =>
              {
                  Teacher update = universityContext.Teachers.Find(ViewModel.selectedItemTeach.Id);
                  update.Surname = SurnameT;
                  update.Discipline = discipline;
                  universityContext.SaveChanges();
                  ViewModel.updateTeacher.Close();
              });
            }
        }
        public ICommand CloseButton
        {
            get
            {
                return new ButtonsCommand(
                    () =>
                    {
                        ViewModel.addStudent?.Close();
                        ViewModel.addTeacher?.Close();
                        ViewModel.updateStudent?.Close();
                        ViewModel.updateTeacher?.Close();
                    }
                    );
            }
        }
    }
}





