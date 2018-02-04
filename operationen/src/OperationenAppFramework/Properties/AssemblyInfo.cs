using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("OperationenAppFramework")]
[assembly: AssemblyDescription("Common Framework for Windows and Web Applications")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Christoph Maurer")]
[assembly: AssemblyProduct("OP-LOG")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//
// AllowPartiallyTrustedCallers is needed or the code will throw exception
// SecurityException: That assembly does not allow partially trusted callers
// when deployed on 1und1 web hosting
//
[assembly: AllowPartiallyTrustedCallers()]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyFileVersion("1.0.0.0")]
