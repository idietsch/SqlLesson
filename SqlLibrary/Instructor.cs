using System;
using System.Collections.Generic;
using System.Text;

namespace SqlLibrary {
    public class Instructor {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearsExperience { get; set; }
        public bool IsTenured { get; set; }


        public override string ToString() {
            return $"{Id}|{FirstName} {LastName}|{YearsExperience}|{IsTenured};";
        }

    }
}
