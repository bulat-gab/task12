# XML manager
WinForm application which allows you to work with XML files: save them to database, edit, load and save to file system.

## Functionality:
- Load .xml file from File System to DB (Right click on the row and chose "Save To Xml" from context menu)
- Fetch data from DB and save it to a File System in .xml (File > Open)
- Show DB contents
- Edit/Add rows in DB (You can edit/add rows directly in the DataGridView)
- Deleting rows (From context menu)

Edit, Add and Delete operations will write changes into DB only after "Save Changes" button is pressed.

 ## XML File Structure
 
 ```
<File FileVersion=«Version»> 
<Name>File_Name</Name>    
<DateTime>Date_Modified</DateTime>    
</File>
```

File name has the following format «XX_YY_ZZ.xml», where:

• XX – russian letters. Lenght <= 100;

• YY – numbers. Values are either 1, 10, or from 14 to 20;

• ZZ – any symbols. Length <= 7.
