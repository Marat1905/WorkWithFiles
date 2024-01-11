namespace FinalTask
{
    [Serializable]
    internal class Student
    {
        /// <summary>Имя </summary>
        public string Name { get; set; }

        /// <summary>Группа </summary>
        public string Group { get; set; }

        /// <summary>Дата рождения</summary>
        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
    }
}
