# Info
useful [link]|(https://stackoverflow.com/questions/40272341/how-to-pass-parameters-to-a-net-core-project-with-dockerfile)

## Commands and Comments

Build an image

-   First command `docker build -t dockedtransactor .`

Test the image

-   Run the command `docker run -it --rm dockedtransactor`

Create the container

-   Run the command `docker create --name transactor dockedtransactor`

Start the container

-   Run command `docker start transactor`

Stop the container

-   Run command `docker stop transactor`
