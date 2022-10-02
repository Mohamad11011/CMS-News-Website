# DgPadCMS
1. Project's Title: DgPad NEWS & CMS Websites

2. Project Description
  This project is a CMS control website for Administrators (Admin/Editor/...) where Role guarantees the access to the CMS exclusively.The other part of this project is another Website (News Website) where users can view and observe the News added from the CMS management system.(It also includes a Library class that holds the layer of data between the management and View layers).
  
  CMS Part: here authorized pages are given to the Adminstration with exclusive pages and actions for Admins only.
      Roles can be added/deleted/edited/assigned-to-users/removed-from-users
      Users can be added/removed/viewed
      News Filtering (Taxonomy-Postype-Terms) can be added/deleted/edited and linked by Many-to-Many or One-to-Many
      
  News Part: here public users can view the News added from the CMS to the DB.
        The News are rendered in a Well Formed and Organized Layouts for a UserFriendly Interface.
        
  Common Library: Here all needed functions are written + the DB Models are supplied in this library 
                  so when needed to be retrieved anytime in both CMS and News.
        
  Some Technologies used are: .Net6 ,razor engine to add functionality by the ( C# ).
                              HTML & CSS are basically used for the Frontend styling.
                              Javascript,Jquery,Ajax,Json formatting are also used to to insure scalable website.
                              SQL for the database management with the needed Linq Queries that facilitates the data retrieving.
                              The Project is a .net MVC patterned ( Model-View-Controller ) which emphasizes a separation between the software's business logic and display.
   
  
                              
  
  Note:
  The Library is added as a reference in both CMS and News Websites to get the needed databse.


