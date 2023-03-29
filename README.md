# Smart Home Solution

This repository contains the source code for a distributed Smart Home application built on .NET using Windows Communication Foundation (WCF) with TCP protocol.

## Overview
The Smart Home solution is built using a component-oriented design and comprises six projects:

- SecuritySystem.Server: An ASP.NET project that serves as the server for the Security System part of the application. It exposes multiple services for the client app to consume via WCF with TCP.

- SecuritySystem.UI: A WPF client application that allows users to interact with the Security System services provided by the server. Through this app, users can turn on/off connected objects, open/close the main door, view live camera feeds, and check the history of security-related events.

- Administration.Server: A server-side project that exposes Administration services for the web app to consume. Like SecuritySystem.Server, it uses WCF with TCP to communicate with the web app.

- Administration.WebApp: An ASP.NET web application that allows users to manage the Smart Home system. Through this app, users can add/modify/delete connected objects and users.

- DoorApp: A WPF client application that will be installed on the door to let people request the door to open. It notifies the SecuritySystem server to open the door.

- SmartHome.Contracts: A project that holds interfaces for the services and model classes used by the other projects.

## Usage
To use the Smart Home solution, you will need to build the solution in Visual Studio and then run the appropriate projects depending on the functionality you want to test.

To run the SecuritySystem.UI or DoorApp clients, you will need to run the SecuritySystem.Server project first. Similarly, the Administration.WebApp requires the Administration.Server project to be running.

## Screenshots
Here are some screenshots of the Smart Home application:

![Security System Dashboard](/screenshots/dashboard.png)

*The main dashboard of the desktop client.*

![Security System Devices](/screenshots/devices.png)


![Security System History](/screenshots/history.png)


![Login Window](/screenshots/login.png)

*Login Window of the desktop client.*

![Devcies management](/screenshots/devices_manage.png)

*Devcies management.*

## Requirements
To build and run the Smart Home solution, you will need:

Visual Studio 2019 or later
.NET 6.0 or later
Windows Communication Foundation (WCF)
## Future Work
Some potential areas for future development of the Smart Home solution include:

- Adding more security-related features such as facial recognition for user authentication or integration with external security systems.
- Adding more connected objects such as smart thermostats or lighting systems.
- Improving the user interfaces of the client applications to be more intuitive and user-friendly.

## License
This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.
