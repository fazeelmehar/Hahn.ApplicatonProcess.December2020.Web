using System;
using System.Collections;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.December2020.Domain.Helpers
{
    public class EntityResponseModel<T>
    {
        public bool ReturnStatus { get; set; }
        public List<string> ReturnMessage { get; set; }
        public List<String> Errors;
        public T Data = default(T);
        public EntityResponseModel()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            Errors = new List<String>();
            Data = default(T);
        }
    }
}
