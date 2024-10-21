<h1>Ecommerce Web App</h1>

<p>This is an Ecommerce Web Application built using .NET Core MVC with two roles: <strong>Admin</strong> and <strong>User</strong>. The application allows users to browse and purchase electronic products and allows admins to manage categories, products, and orders.</p>

<h2>Features</h2>

<h3>User Side:</h3>
    <ul>
        <li><strong>Browse Products:</strong> Users can see all available electronic items such as mobiles, laptops, and TVs.</li>
        <li><strong>View Product Details:</strong> Users can view individual product details on a dedicated product page.</li>
        <li><strong>Add to Cart:</strong> Users can add items to their cart from the product detail page.</li>
        <li><strong>View Cart:</strong> Users can view their cart, see the summary of items, and proceed to checkout.</li>
        <li><strong>Place Orders:</strong> Users can place orders after reviewing their cart.</li>
        <li><strong>Order History:</strong> Users can view a list of their past orders.</li>
    </ul>

  <h3>Admin Side:</h3>
  <ul>
      <li><strong>Manage Categories:</strong> Admins can create, update, delete, and view product categories.</li>
      <li><strong>Manage Products:</strong> Admins can add, edit, delete, and view products.</li>
      <li><strong>Manage Orders:</strong> Admins can view all orders placed by users and manage their status.</li>
  </ul>

  <h2>Technologies Used</h2>
  <ul>
      <li><strong>Frontend:</strong> HTML, CSS, Bootstrap, Razor Views</li>
      <li><strong>Backend:</strong> .NET Core MVC</li>
      <li><strong>Database:</strong> Entity Framework Core with SQL Server</li>
      <li><strong>Authentication:</strong> ASP.NET Core Identity for role-based access (Admin and User)</li>
  </ul>

  <h2>Setup Instructions</h2>

  <h3>Prerequisites:</h3>
  <ul>
      <li><a href="https://dotnet.microsoft.com/download" target="_blank">.NET SDK</a> (version 8.0)</li>
      <li><a href="https://www.microsoft.com/en-us/sql-server/sql-server-downloads" target="_blank">SQL Server</a></li>
      <li>Visual Studio (optional for development)</li>
  </ul>

  <h3>Installation:</h3>
  <ol>
      <li>Clone this repository:
          <pre><code>git clone https://github.com/your-repo/ecommerce-webapp.git
cd ecommerce-webapp
            </code></pre>
        </li>
        <li>Install the required dependencies:
            <pre><code>dotnet restore
            </code></pre>
        </li>
        <li>Set up the database:
            <ul>
                <li>Update the <code>appsettings.json</code> file with your SQL Server connection string.</li>
                <li>Run the migrations to set up the database schema:
                    <pre><code>dotnet ef database update
                    </code></pre>
                </li>
            </ul>
        </li>
        <li>Run the application:
            <pre><code>dotnet run
            </code></pre>
        </li>
        <li>Navigate to <a href="https://localhost:44309" target="_blank">https://localhost:44309</a> to view the application.</li>
    </ol>

  <h2>Project Structure</h2>
  <ul>
      <li><strong>Areas/Admin:</strong> Contains controllers and views for admin management (Categories, Products, Orders).</li>
      <li><strong>Models:</strong> Contains the application data models (Category, Product, Order, etc.).</li>
      <li><strong>Data:</strong> Handles the database context and migrations.</li>
      <li><strong>Repository Pattern:</strong> Used for the data access layer with UnitOfWork.</li>
  </ul>

  <h2>Contributers</h2>
<table>
  <tr>
    <td align="center">
      <a href="https://github.com/karangondaliya">
        <img src="https://github.com/karangondaliya.png" width="100px;" alt="Profile Photo"/><br />
        <sub><b>Karan Gondaliya</b></sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/RushangG">
        <img src="https://github.com/RushangG.png" width="100px;" alt="Profile Photo"/><br />
        <sub><b>Rushang Gajera</b></sub>
      </a>
    </td>
    <!-- Add more contributors here -->
  </tr>
</table>


</body>
</html>
