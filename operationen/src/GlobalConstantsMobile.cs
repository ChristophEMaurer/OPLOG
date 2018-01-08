using System;
using System.Collections.Generic;
using System.Text;

namespace Operationen
{
    /// <summary>
    /// This file is referenced as a linked source by other projects
    /// </summary>
    sealed public class GlobalConstantsMobile
    {
        /// <summary>
        /// File format for OP catalog
        /// 
        /// Format is
        /// _P_|1
        /// [number of lines following]
        /// 5-500.x|Inzision der Leber: SonstigeT
        /// 
        /// 
        /// _P_|2
        /// [number N of groups]
        /// group1
        /// [number of entries M1 in group1]
        /// 5-500.x|Inzision der Leber: SonstigeT
        /// ...
        /// 
        /// groupN
        /// [number of entries in groupN]
        /// 5-501.x|Inzision der Leber: bla bla
        /// ...
        /// </summary>
        public const string FileSignatureOPSKatalogMobile = "_P_|2";
        public const string FileIdOPSKatalogMobile = "_P_";
        public const int FileVersionOPSKatalogMobile = 2;

        /// <summary>
        /// File Signature for OPs of one surgeon
        /// 
        /// Format is
        /// _U_|1
        /// 20090330 09:55 5-500.x|Inzision der Leber: SonstigeT
        /// </summary>
        public const string FileSignatureOPSUserMobile = "_U_|1";

        //
        // On the iPhone we can only select file with this extension
        //
        public const string FILE_EXTENSION = "oplog";
    }
}

