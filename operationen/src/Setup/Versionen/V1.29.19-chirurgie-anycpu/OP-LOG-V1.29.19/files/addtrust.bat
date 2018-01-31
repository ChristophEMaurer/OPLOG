c:
cd %windir%\Microsoft.NET\Framework\v2.0.50727
caspol.exe -q -machine -addgroup "All_Code" -url "$__PLACEHOLDER__PROGRAM__$\*" FullTrust -n "oplog"
caspol.exe -q -machine -addgroup "All_Code" -url "$__PLACEHOLDER__DATA__$\*" FullTrust -n "oplogData"

