### **MSSQL Password**
```yourStrong(*)Password```

### **Scaffold Command**
in case database changes
```
dotnet ef dbcontext scaffold "Server=localhost;Database=DB_6215_19_S1;User=sa;Password=yourStrong(*)Password;" Microsoft.EntityFrameworkCore.SqlServer -o Models -f -c MoviesContext
```

### **NEXT STEPS**
open new wpf project in Visual Studio. 
Go to 'packages' & search NUGET for Newtonsoft package. 
Add to project and add 'using Newtonsoft;' statement. 
Localhost/5000/api/.... will be the source of the JSON data.

