# WoodCraft Furniture - E-Commerce Website

A modern, responsive furniture e-commerce website built with ASP.NET Core MVC, Entity Framework Core, MySQL, and Bootstrap 5.

## Features

- **Homepage** - Modern design with featured furniture and category navigation
- **Product Catalog** - Browse products by categories (Sofas, Beds, Tables, Chairs, Wardrobes)
- **Product Search** - Search products by name or description
- **Product Details** - View detailed product information with images and pricing
- **Shopping Cart** - Add products, update quantities, remove items, calculate total
- **User Authentication** - Customer registration and login
- **Admin Dashboard** - Add, edit, and delete furniture products
- **Responsive Design** - Works on desktop, tablet, and mobile devices
- **MySQL Database** - Data persistence with Entity Framework Core

## Technology Stack

- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Backend**: C# with ASP.NET Core MVC (.NET 6.0)
- **Database**: MySQL with Entity Framework Core 6.0
- **ORM**: Entity Framework Core with Pomelo MySQL Provider

## Prerequisites

Before running this project, ensure you have the following installed:

1. [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. [MySQL Server](https://dev.mysql.com/downloads/mysql/) (8.0+ recommended)
3. [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) (optional, for database management)
4. A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Setup Instructions

### 1. Clone or Download the Project

Extract the project to your desired location: `C:\Users\manis\Downloads\furniture\WoodCraftFurniture\`

### 2. Configure Database Connection

Open `appsettings.json` and update the connection string with your MySQL credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=WoodCraftDb;User=root;Password=yourpassword;"
  }
}
```

Replace `yourpassword` with your MySQL root password.

### 3. Create the Database

Open a terminal/command prompt in the project directory and run the following commands:

```bash
# Install EF Core tools globally (if not already installed)
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

### 4. Run the Application

```bash
# Run the application
dotnet run
```

The application will be available at:
- HTTPS: `https://localhost:7000`
- HTTP: `http://localhost:5000`

## Default Admin Account

To access the admin dashboard, you'll need to create an admin user. You can do this by:

1. Register a new user through the website
2. Use MySQL Workbench or CLI to update the user in the database:
```sql
UPDATE Users SET IsAdmin = 1 WHERE Email = 'your-email@example.com';
```

## Project Structure

```
WoodCraftFurniture/
├── Controllers/        # MVC Controllers
├── Data/              # DbContext
├── Models/            # Data Models
├── Views/             # Razor Views
│   ├── Home/
│   ├── Products/
│   ├── Cart/
│   ├── Account/
│   ├── Admin/
│   └── Shared/
├── wwwroot/           # Static files
│   ├── css/
│   ├── js/
│   └── images/
├── Program.cs         # Application entry point
├── appsettings.json   # Configuration
└── WoodCraftFurniture.csproj
```

## Usage

1. **Browse Products**: Visit the Products page or use category filters
2. **Search**: Use the search bar to find specific products
3. **Add to Cart**: Click "Add to Cart" on any product
4. **Manage Cart**: Update quantities or remove items from the cart page
5. **Register/Login**: Create an account to make purchases
6. **Admin Features**: Login as admin to manage products via Admin Dashboard

## Notes

- This is a beginner-friendly project with clean, simple code
- Product images use placeholder URLs from Unsplash; you can replace them with your own
- The checkout process is simplified (button present but no payment integration)
- Session is used for cart management
- Passwords are stored as plain text (for educational purposes only - use hashing in production)

## Troubleshooting

- **Database connection errors**: Ensure MySQL is running and credentials are correct
- **Migration errors**: Make sure EF Core tools are installed and .NET SDK is correct
- **Port conflicts**: Check if ports 5000/7000 are available

Enjoy your WoodCraft Furniture website!
