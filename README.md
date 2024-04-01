Description:
Dev Blog is an ASP.NET MVC project designed for creating and managing a development-oriented blog. It features multiple user roles, including SuperAdmin, Admin, and User, each with specific permissions and functionalities. The project incorporates essential features such as user authentication, blog post management, liking, commenting, and integration with external services like Floroala for rich text editing and Cloudinary for image uploads.

User Roles:

SuperAdmin: Can create both Admin and User accounts, manage blog posts (CRUD operations), and CRUD (Create, Update, Delete) tags. SuperAdmin has access to all functionalities.
Admin: Can create User accounts, manage blog posts (CRUD operations), and CRUD tags. Admin can also access functionalities available to Users.
User: Can like and comment on blog posts. Users must be logged in to like and comment.
Authentication:

SuperAdmin can log in as SuperAdmin, Admin, and User.
Admin can log in as Admin and User.
User can only log in as User.
Features (Implemented and Under Development):

User Management: SuperAdmin can create and manage both Admin and User accounts.
Blog Post Management: SuperAdmin and Admin can create, update, and delete blog posts (CRUD operations).
Tag Management: SuperAdmin and Admin can CRUD tags for organizing blog posts.
User Interaction: Users can like and comment on blog posts, with restrictions on one like per user per blog post. Liking and commenting require user authentication.
Rich Text Editing: Integration with Floroala for a rich text editor.
Image Upload: Integration with Cloudinary for uploading images in blog posts.
Repository Pattern: Utilizes the repository pattern for data access.
Entity Framework Core: Uses EF Core for database operations.
Microsoft Identity: Integrates Microsoft Identity for user authentication and authorization.
Validation: Implements both client-side and server-side validation for user inputs.
Sorting and Pagination: Under development to facilitate better organization and navigation within the blog.
