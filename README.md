# Dev Blog - ASP.NET MVC Project

## Description
Dev Blog is an ASP.NET MVC project designed for creating and managing a development-oriented blog. It features multiple user roles, including SuperAdmin, Admin, and User, each with specific permissions and functionalities. The project incorporates essential features such as user authentication, blog post management, liking, commenting, and integration with external services like Floroala for rich text editing and Cloudinary for image uploads.

## User Roles
1. **SuperAdmin:** Can create both Admin and User accounts, manage blog posts (CRUD operations), and CRUD (Create, Update, Delete) tags. SuperAdmin has access to all functionalities.
2. **Admin:** Can create User accounts, manage blog posts (CRUD operations), and CRUD tags. Admin can also access functionalities available to Users.
3. **User:** Can like and comment on blog posts. Users must be logged in to like and comment.

## Authentication
- SuperAdmin can log in as SuperAdmin, Admin, and User.
- Admin can log in as Admin and User.
- User can only log in as User.

## Features (Implemented)
1. **User Management:** SuperAdmin can create and manage both Admin and User accounts.
2. **Blog Post Management:** SuperAdmin and Admin can create, update, and delete blog posts (CRUD operations).
3. **Tag Management:** SuperAdmin and Admin can CRUD tags for organizing blog posts.
4. **User Interaction:** Users can like and comment on blog posts, with restrictions on one like per user per blog post. Liking and commenting require user authentication.
5. **Rich Text Editing:** Integration with Floroala for a rich text editor.
6. **Image Upload:** Integration with Cloudinary for uploading images in blog posts.
7. **Repository Pattern:** Utilizes the repository pattern for data access.
8. **Entity Framework Core:** Uses EF Core for database operations.
9. **Microsoft Identity:** Integrates Microsoft Identity for user authentication and authorization.
10. **Validation:** Implements both client-side and server-side validation for user inputs.
11. **Sorting and Pagination:**   facilitate better organization and navigation within the blog.
