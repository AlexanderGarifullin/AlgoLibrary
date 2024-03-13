# Algolibrary

Algolibrary is a web application designed for studying algorithms. It serves as a course project for the 3rd year of study at the Higher School of Economics Perm. This application provides users with the opportunity to review **concise summaries** of algorithms and reinforce their learning through practical exercises.

## Features

- **Algorithm Concpects**: Users can read concise summaries of various algorithms.
- **Practice**: Practical exercises are available through a group on the Codeforces website. Each summary includes links to corresponding exercises in this group.
- **Organized Content**: Summaries are categorized by topics, making it easier for users to navigate and find relevant information.
- **Study Plan**: A structured plan for learning algorithms is provided, with algorithms categorized based on their level of difficulty.
- **User Roles**: The application supports three roles: User, Editor, and Admin. Each role has different permissions, ranging from viewing data to managing content and users.
- **Markdown Support**: Summaries are stored in Markdown format and converted to HTML for display.
- **Security**: Passwords are encrypted using the AES algorithm for enhanced security.

## Screenshots

Here are some screenshots of Algolibrary in action:

![main](https://github.com/AlexanderGarifullin/AlgoLibrary/assets/139747118/1f1e344a-8db1-4580-8339-37d6a7b88e99)
*Welcome page of the web application.*

![plan](https://github.com/AlexanderGarifullin/AlgoLibrary/assets/139747118/d9c7cdcc-5dd6-4685-976e-9cb831cbf57e)
*Welcome page of the study plan.*

![login](https://github.com/AlexanderGarifullin/AlgoLibrary/assets/139747118/df041db2-4e1d-400e-87ed-ebdc0673df4a)
*Login Page.*

![theme-control](https://github.com/AlexanderGarifullin/AlgoLibrary/assets/139747118/40440895-6de0-4259-ad52-8d4a75fbe992)
*Topics Management Page.*

![folder-controller](https://github.com/AlexanderGarifullin/AlgoLibrary/assets/139747118/be3c86c7-5474-4c2c-89ee-01966c1057a4)
*Study Plan Folders Management Page.*

![conscpecr-controller](https://github.com/AlexanderGarifullin/AlgoLibrary/assets/139747118/7ca6de97-2236-4d81-aa09-ee767d253833)
*Algorithm Summaries Management Page:.*

![conspect-folder-controller](https://github.com/AlexanderGarifullin/AlgoLibrary/assets/139747118/ec2ea18c-0443-4194-b247-8a386316f759)
*Folder Contents Management Page.*

## Technologies Used

- **ASP .NET CORE MVC**: The application is built using ASP .NET CORE MVC framework, providing a robust and scalable architecture for web development.
- **MySQL Database**: Data is stored in a MySQL database, with Entity Framework utilized for database interaction.
- **Frontend**: Pure CSS and JavaScript are used for styling and client-side interactions, ensuring a smooth and responsive user experience.
- **Markdown Processing**: Markdig library is employed for processing Markdown content, enabling seamless conversion to HTML for display.

## Roles and Permissions

- **User**: Can view algorithm summaries and practice exercises.
- **Editor**: Can add, edit, and delete summaries, topics, and study plan folders.
- **Admin**: Has full control, including adding, deleting, editing, and viewing all users.

## Usage

You can access the Algolibrary web application [here](https://a25289-7683.u.d-f.pw).
You can access the group for practice exercises on Codeforces [here](https://codeforces.com/group/eYghlTf5Xb/contests).

## Contributing

Contributions to Algolibrary are welcome! If you have suggestions for improvements or would like to report any issues, please feel free to open an issue or submit a pull request on GitHub.

## License

This project is licensed under the [Apache License](LICENSE). Feel free to use, modify, and distribute the code as per the terms of the license.
