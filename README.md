# WebApplication

# Email Notification Service Readme

## Overview
This repository contains the code for an Email Notification Service, which is designed to handle email notifications triggered by a web application. The service utilizes Azure Service Bus as the message queue provider, ensuring reliable message delivery and scalability.

## Components
1. **Web Application**
   - URL: http://api/usercontroller/emailrequest
   - Purpose: Push messages to the Azure Service Bus queue.

2. **EmailAPI Project**
   - Purpose: Consume messages from the Azure Service Bus queue.
   - Actions:
     - Log incoming messages in the database.
     - Send email notifications based on the message content.

## Technologies Used
- C# for backend development.
- Azure Service Bus for message queuing.
- Entity Framework Core for database operations.
- SMTP protocol for sending emails.

## Setup Instructions
1. Clone the repository to your local machine.
2. Ensure you have Visual Studio installed for C# development.
3. Configure the Azure Service Bus connection string in the EmailAPI project.
4. Update the database connection string in the EmailAPI project to point to your database.
5. Build and run the EmailAPI project to start consuming messages from the queue.

## Folder Structure

## Usage
1. **Web Application Usage**
   - Access the web application at http://api/usercontroller/emailrequest.
   - Trigger actions that generate messages to be sent to the queue.

2. **EmailAPI Usage**
   - Automatically consumes messages from the Azure Service Bus queue.
   - Logs messages in the database and sends email notifications.

## Contributing
Contributions are welcome! Please follow the guidelines in CONTRIBUTING.md.

## License
This project is licensed under the MIT License - see the LICENSE.md file for details.

## Contact
For any inquiries or support, please contact [Your Email Address].


