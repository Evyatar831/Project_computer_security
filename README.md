# Secure Customer Management System for Communication_LTD

## Project Overview
This project is a secure web application developed for a fictional telecommunications company called Communication_LTD. The application facilitates customer management, featuring robust security measures to protect against common web vulnerabilities like XSS (Cross-Site Scripting) and SQL Injection.

## Features

### User Authentication & Security
- **Secure Registration**: Implementation of a comprehensive user registration system with validation
- **Password Complexity Enforcement**: Configurable password requirements including length, character types, and uniqueness
- **Secure Password Storage**: HMAC-SHA256 with salting for secure password hashing
- **Password History**: Prevention of password reuse (last 3 passwords)
- **Account Lockout**: Protection against brute force attacks (5 failed attempts, 15-minute lockout)
- **Password Reset Workflow**: Secure password recovery mechanism

### Customer Management
- **Customer Data Management**: Add, update, and delete customer records
- **Data Validation**: Input validation to prevent malformed data
- **Security Controls**: Implemented protection against XSS and SQL Injection attacks
- **Admin Dashboard**: Intuitive interface for managing customer data

## Security Implementation
- **XSS Protection**: Comprehensive encoding of user inputs and outputs
- **SQL Injection Prevention**: Parameterized queries for all database operations
- **Input Sanitization**: Proper sanitization of user inputs
- **HTTPS Support**: Configuration for secure communications

## Technical Details
- **Framework**: ASP.NET Web Forms (C#)
- **Database**: MySQL
- **ORM**: Direct SQL with parameterized queries
- **Frontend**: HTML, CSS, JavaScript with Bootstrap for responsive design

## Project Structure
- **Default.aspx**: Login page
- **Registration.aspx**: User registration
- **Logged_in.aspx**: Main dashboard for customer management
- **Change_Password.aspx**: Password change functionality
- **Forgot_password.aspx**: Password recovery workflow
- **Reset_password.aspx**: Password reset implementation
- **SecurityUtilities.cs**: Security helper methods
- **SecurePasswordHandler.cs**: Password management and validation

## Getting Started

### Prerequisites
- Visual Studio (2019 or later recommended)
- .NET Framework 4.7.2
- MySQL Server and MySQL Workbench

### Installation
1. Clone the repository to your local machine
2. Open MySQL Workbench and create a new schema called `webapp`
3. Execute the following SQL commands to create a database user:
   ```sql
   CREATE USER 'admin'@'localhost' IDENTIFIED BY 'admin';
   GRANT ALL PRIVILEGES ON *.* TO 'admin'@'localhost' WITH GRANT OPTION;
   CREATE USER 'admin'@'%' IDENTIFIED BY 'admin';
   GRANT ALL PRIVILEGES ON *.* TO 'admin'@'%' WITH GRANT OPTION;
   SELECT user, host FROM mysql.user;
   ```
4. Open the solution file in Visual Studio
5. Build the solution to restore NuGet packages
6. Run the project using default.aspx as the startup page

### Configuration
- Password policies can be configured in the `PasswordValidationRules.txt` file
- Database connection settings are in the Web.config file
- The application is pre-configured to use local MySQL with the admin credentials created above

## Important Security Notes
- This project demonstrates both vulnerable and secure implementations for educational purposes
- The secure version implements proper defenses against XSS and SQL Injection attacks
- For production use, additional security measures should be implemented:
  - Use HTTPS exclusively
  - Implement CSRF protection
  - Consider using a dedicated authentication framework
  - Update all dependencies regularly
  - Do not use 'admin' as a password in production

## License
This project is for educational purposes. Please use responsibly.

## Acknowledgments
This project was developed as part of a cybersecurity course to demonstrate secure coding practices in web applications.
