System Architecture Documentation
Overview
The system follows a clean architecture to achieve scalability, maintainability, and flexibility. Key architectural components include:
•	Backend Services:
•	Tachograph Data Service:
•	Handles the processing and storage of tachograph data through uploading file containing tachograph data in json format.
•	Retrieve different types of drive violations based on the uploaded data file following the below data format.
•	Developed using .NET 8 .
•	Utilizes Entity Framework Core for database interaction.
•	Database:
•	PostgreSQL is used as the primary relational database for persistent data storage.
Protocols Used
•	HTTP/HTTPS:
•	Used for communication between the frontend and backend services.
•	Ensures secure data transfer.
•	RESTful API:
•	RESTful principles are followed for designing APIs.
•	Endpoints are structured for resource manipulation.
•	JSON:
•	Data exchange format for communication between frontend and backend.
•	Swagger:
•	API documentation and testing using Swagger specifications.
Compliance Measures
•	Logging and Monitoring:
•	Comprehensive logging is implemented for monitoring system behavior.
•	Monitoring tools track system performance and notify of potential issues.
•	Continuous Compliance Monitoring:
•	Regularly review and update compliance measures based on evolving regulations and best practices.

Database Local Setup 
•	You should install postgresql
•	Install visual studio 2022 
•	Clone this repository https://github.com/OmarHamdyF/Tachograph.git
•	Open the cloned project using visual studio 2022
•	Open Package manager console , then run “update-database”
•	Run project , a swagger page will be opened
•	Upload text file to “upload” api then you can test the rest apis 
•	The data format should be applied to the uploaded file 
[
  {
    "DriverId": "Driver A",
    "Date": "2023-11-01",
    "StartTime": "08:00:00",
    "EndTime": "09:00:00",
    "Activity": "Driving"
  },
  {
    "DriverId": "Driver A",
    "Date": "2023-11-01",
    "StartTime": "09:00:00",
    "EndTime": "09:30:00",
    "Activity": "Rest"
  }
]
If you need to add data , there is data seeding for driver ids (Driver A , Driver B)  
Fill all the required data and the only available Activity status are (Driving , Rest)
