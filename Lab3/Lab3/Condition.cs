﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Condition : Statement
    {
        public Condition(string variable, FuzzySet term)
        {
            this.Variable = new Variable(variable);
            this.Term = term;
        }
    }
}
