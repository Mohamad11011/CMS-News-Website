# DgPadCMS
1. Project's Title: DgPad NEWS & CMS Websites

2. Project Description
  This project is a CMS control website for Administrators (Admin/Editor/...) where Role guarantees the access to the CMS exclusively.The other part of this project is another Website (News Website) where users can view and observe the News added from the CMS management system.(It also includes a Library class that holds the layer of data between the management and View layers).
  
  CMS Part: here authorized pages are given to the Adminstration with exclusive pages and actions for Admins only.
      Roles can be added/deleted/edited/assigned-to-users/removed-from-users
      Users can be added/removed/viewed
      News Filtering (Taxonomy-Postype-Terms) can be added/deleted/edited and linked by Many-to-Many or One-to-Many
      
  News Part: here public users can view the News added from the CMS to the DB.
        Search & Filtering are found for the user.
        The News are rendered in a Well Formed and Organized Layouts for a UserFriendly Interface.
        
  Common Library: Here all needed functions are written + the DB Models are supplied in this library 
                  so when needed to be retrieved anytime in both CMS and News.
        
  Some Technologies used are: .Net6 ,razor engine to add functionality by the ( C# ).
                              HTML & CSS are basically used for the Frontend styling.
                              Javascript,Jquery,Ajax,Json formatting are also used to to insure scalable website.
                              SQL for the database management with the needed Linq Queries that facilitates the data retrieving.
                              The Project is a .net MVC patterned ( Model-View-Controller ) which emphasizes a separation between the software's logic and display.
   
  
  Note:
  The Library is added as a reference in both CMS and News Websites to get the needed databse.
  Some Pictures below:
  1.Admin CMS 
  
![Login](https://user-images.githubusercontent.com/107344706/197824245-7efb3fa5-ec56-4122-a516-eae25d20c351.png)
Authorized login

![AdminEditor](https://user-images.githubusercontent.com/107344706/197824300-c40d074a-6e80-4c6a-87b6-4aa2d04992aa.png)
Posts Page with filtering

![CreatePost1](https://user-images.githubusercontent.com/107344706/197824492-c3735a01-95d4-4d3a-9e7f-5f64b1409aa7.png)

![CreatePost2](https://user-images.githubusercontent.com/107344706/197824542-40a09ffe-fb3b-417d-933c-86b22bd21d32.png)
![CreateVideoLink](https://user-images.githubusercontent.com/107344706/197824563-ccb9b032-b91f-429b-b015-4a046c9109e1.png)
Posts Creation dynamic Form

2.User Side Public News Website


![Loader1](https://user-images.githubusercontent.com/107344706/197824940-8b3f0c87-5a95-488f-89ae-ccda8ff40ea0.png)
Loader

![Main1](https://user-images.githubusercontent.com/107344706/197824977-e95cfb62-2c2b-4bfc-939f-9d4d22bd57eb.png)
![Main2](https://user-images.githubusercontent.com/107344706/197825100-6296bdd9-9cc9-45b4-b47a-9d2f9076b6a8.png)
![Main3](https://user-images.githubusercontent.com/107344706/197825113-63e34fe5-47c4-423b-812c-66dae19fdaed.png)
Main Page

![DetailsYoutube](https://user-images.githubusercontent.com/107344706/197825191-3f53949b-5313-4f03-8781-13468bb46add.png)
Details Page of Selected News







