In order to run the project on your computer use the following steps:
1. Open MySQL workbench
2. Use the SQL file in order to make the wanted schema.
4. Run the following query on new SQL tab:
   
create user 'admin'@'localhost'
identified by 'admin';
grant all privileges on *.* to 'admin'@'localhost' with grant option;
create user 'admin'@'%' identified by 'admin';
grant all privileges on *.* to 'admin'@'%' with grant option;
select user, host from mysql.user;

(Please wrap the dot (.) with * after grant all privileges on ... Make sure you do it for both lines )


4. The connection string with the right setup is already configured in the web.config file.
5. Run the project from the default.aspx file (It should run from there automatically).
   
