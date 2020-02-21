using System;
using System.Collections.Generic;
using System.Text;

namespace SqlLibrary {
    public class Class {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int Section { get; set; }
        public bool InstructorId { get; set; }

        public override string ToString() {
            return $"{Id}|{Subject}|{Section}|{InstructorId};";
        }
    }
}
