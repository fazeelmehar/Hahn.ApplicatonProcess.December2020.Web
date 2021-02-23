using System;
using System.Collections;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.December2020.Domain.Helpers
{
    public class EntityResponseListModel<T>
    {
        public bool ReturnStatus { get; set; }
        public List<string> ReturnMessage { get; set; }
        public List<String> Errors;
        public List<T> Data = default(List<T>);
        public EntityResponseListModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new List<String>();
            Data = default(List<T>);
        }
    }
}
