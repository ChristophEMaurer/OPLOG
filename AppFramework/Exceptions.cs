using System;
using System.Runtime.Serialization;

namespace AppFramework
{
    namespace Exceptions
    {
        [SerializableAttribute]
        public class InvalidInputException : System.Exception 
        {
            public InvalidInputException()
            {
            }
                
            public InvalidInputException(string message) : 
                base(message) 
            {
            }

            public InvalidInputException(string message, Exception innerException) : 
             base (message, innerException)
            {
            }

            protected InvalidInputException(SerializationInfo info, StreamingContext context) : 
              base(info, context)
            {
            }
        }

        [SerializableAttribute]
        public class ValueDoesNotExistException : System.Exception 
        { 
            public ValueDoesNotExistException()
            {
            }
                
            public ValueDoesNotExistException(string message): base(message) 
            {
            }

            public ValueDoesNotExistException(string message, Exception innerException) : 
                base (message, innerException)
            {
            }

            protected ValueDoesNotExistException(SerializationInfo info, StreamingContext context) :
                base(info, context)
            {
            }        
        }

        [SerializableAttribute]
        public class KeyDoesNotExistException : System.Exception 
        { 
            public KeyDoesNotExistException()
            {
            }
                
            public KeyDoesNotExistException(string message) : 
                base(message) 
            {
            }

            public KeyDoesNotExistException(string message, Exception innerException) : 
                base (message, innerException)
            {
            }

            protected KeyDoesNotExistException(SerializationInfo info, StreamingContext context) :
                base(info, context)
            {
            }      
        }
    }
}
