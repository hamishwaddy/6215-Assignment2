### **MSSQL Password**
```yourStrong(*)Password```

### **Scaffold Command**
in case database changes
```
dotnet ef dbcontext scaffold "Server=localhost;Database=DB_6215_19_S1;User=sa;Password=yourStrong(*)Password;" Microsoft.EntityFrameworkCore.SqlServer -o Models -f -c MoviesContext
```

### **NEXT STEPS**
https://docs.jwk.nz/dotnet_core/sqlsvr_webapi/dotnet-2.1.x/04-testing-the-api/
Test API routes using VS Code and/or Postman using this link.

