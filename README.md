# Welcome to the Cisco Config Backup Collector / RemoteWork wiki!

RemoteWork - Telnet and SSH auto command executor and configuration downloads. 
Usage for Cisco, Juniper other network devices and UNIX-like machines for easy and fast device configuration backups and management.

## Technologies
Main technologies:
* .Net Framework 4.5 
* C# 5.0
* MS SQL Server Express 2012
* Visual Studio 2013
* Windows Forms
* Entify Framework 6.0

Additional libraries:
* LinqToExcel
* SharpSnmpLib
* DiffPlex

## Structure
Project include 6 modules:
* RemoteWork - WinForms GUI
* RemoteWorkUtil - Console app for easy task scheduling
* RemoteWork.Access - EF configuration
* RemoteWork.Data - Model implemetation
* RemoteWork.Expect - SSH and Telnet connection algorithms
* RemoteWork.Collector - Console app for fast configuration and backup as RANCID or ANSIBLE (not implemented yet)
* WebAppRemoteConfig - ASP.NET MVC app (not implemented yet)
* WebAppRemoteConfig.Tests - Unit tests for MVC app (not implemented yet)

## Authorisation
Has 4 authorisation modes for Telnet protocol: 
* simple (username and password)
* simple with enable mode (username, password, enable password)
* anonymous (only password)
* anonymous with enable mode (password, enable password)

Has 2 authorisation modes for SSH protocol:
* simple
* simple with enable mode.

##Benefits
Application has several benefits:
* Customisable timeout for each device
* Extended authorisation mode
* Customisable command order
* Multithreading connections (over 550 devices configuration backup in 3 minutes with 4 core CPU )
* Differ tools for configurations






