using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

[assembly: CLSCompliant(true)]

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Utility")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("OP-LOG")]
[assembly: AssemblyCopyright("Copyright © Christoph Maurer")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//
// AllowPartiallyTrustedCallers is needed or the code will throw exception
// SecurityException: That assembly does not allow partially trusted callers
// when deployed on 1und1 web hosting
//
[assembly: AllowPartiallyTrustedCallers()]

// The following GUID is for the ID of the typelib if this project is exposed to COM
//[assembly: Guid("028b2508-178a-43ba-aa44-fedf5cbab3f1")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.1.*")]
