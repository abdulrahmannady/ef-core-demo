# ef-core-demo
Creating demo for entity framework core and how the tool creating tables under the hood by generating code from models<br>
great tip while desiging database schema:<br> 
try to avoid using nvarchar(MAX) unless you really really need it<br>
aside tip : MAX is up to 2 giga byte and as we know that table row can only has 8,060 bytes so if the size of data hit this number sql server management system will allocate the data on file on the desk and add pointer on the row to point to the actual data.<br>
as we know varchar is variable length which means if we add like city with 40 characters it will save only the 40 character. so why don't we just make all column nvarchar(MAX) ?!
the answer is process power issue if we try to lookup some data the sql server management system assume that nvarchar(MAX) is half fall so that it would allocate for example MAX"2giga"/2 size in our RAM memory to read these data just for 1 query<br>
please refere to this article https://www.sqlservercentral.com/forums/topic/nvarchar4000-and-performance for futher information
