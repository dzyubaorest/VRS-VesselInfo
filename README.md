# Vessel Data Transmission
## Problem: there are vessels which send data (IMO Number, Vessel name, Date & Time, Position) every 5th or 4th hour. What we nedd is application for receiving, saving, displaying and editing information.
## Solution: API endpoint receives data, sends it to queue, queue is processed by another process (console appliation) which saves data to sql server database. Web project renders information and allows to edit it.
###### Prerequisites: RabbitMq server, startup options: start Console, WebAPi and Web applications simultaneously.