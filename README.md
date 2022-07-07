## Vessel Data Transmission
#### Problem: there are vessels which send data (number, name, position, etc.) every 5th or 4th hour.<br />Application should be created for receiving, saving, displaying and editing information.
#### Solution: API endpoint receives data and sends it to message queue.<br />Queue is processed by another process (Console Appliation).<br />Console Appliation saves data to sql server database.<br />Web project renders information and allows to edit it.
###### Prerequisites: RabbitMq server, startup options: start Console, WebAPi and Web applications simultaneously.