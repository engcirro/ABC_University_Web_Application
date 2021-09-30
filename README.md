ABC University Web Application makes everything that an actual university web application to manage the enrollments processes. 

To run this project properly, you need to configure the Wep.config file to set the database connection according to your PC.
If neeeded,  in the Web.config filelocate the following lines of code  and configure it if needed:
  <connectionStrings>
    <add name="FictionContext" connectionString="Data Source=.;Initial Catalog=FictionUniversityDB;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
  </connectionStrings>
  
  
  
  
