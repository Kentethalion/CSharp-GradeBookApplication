using System;
using System.Collections.Generic;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var letterGrade = 'F';
            var grade = 0;
            var numStudents = Convert.ToDouble(Students.Count);

            if (numStudents < 5)
            {
                throw new System.InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var stepCount = Convert.ToInt32(Math.Floor(numStudents * .2));
            var gradeList = new List<double>();

            foreach(var student in Students)
            {
                gradeList.Add(student.AverageGrade);
            }

            gradeList.Sort((a, b) => b.CompareTo(a));

            for(var i = stepCount; i < gradeList.Count; i += stepCount)
            {
                if(averageGrade > gradeList[i])
                {
                    grade = i;
                    break;
                }
            }

            switch (grade/stepCount)
            {
                case 1:
                    letterGrade = 'A';
                    break;
                case 2:
                    letterGrade = 'B';
                    break;
                case 3:
                    letterGrade = 'C';
                    break;
                case 4:
                    letterGrade = 'D';
                    break;
            }

            return letterGrade;
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                throw new System.InvalidOperationException("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                throw new System.InvalidOperationException("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
