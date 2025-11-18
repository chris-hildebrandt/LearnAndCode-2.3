// Bad example of the open-closed principle

export type Book = {
    title: string;
    genre: string;
}

export type Library = {
    [key: string]: Book[];
}

// This function is not closed for modification
// because we have to modify it every time we
// add a new genre of book to our library
function addBookToLibrary(book: Book, library: Library) {
    switch (book.genre) {
        case 'fiction':
            if (!library['fiction']) {
                library['fiction'] = [];
            }
            library['fiction'].push(book);
            break;
        case 'non-fiction':
            if (!library['non-fiction']) {
                library['non-fiction'] = [];
            }
            library['non-fiction'].push(book);
            break;
        case 'fantasy':
            if (!library['fantasy']) {
                library['fantasy'] = [];
            }
            library['fantasy'].push(book);
            break;
        case 'science-fiction':
            if (!library['science-fiction']) {
                library['science-fiction'] = [];
            }
            library['science-fiction'].push(book);
            break;
        default:
            if (!library['other']) {
                library['other'] = [];
            }
            library['other'].push(book);
            break;
    }
}

/* This function is closed for modification
   because we can add new genres of book to our
   library without modifying this function
*/

function addBookToLibraryRefactored(book: Book, library: Library) {
    if (!library[book.genre]) {
        library[book.genre] = [];
    }

    library[book.genre].push(book);
    return library;
}
/* 
    What changed? Well we added a new parameter to the function
    which is the library object. We then check if the genre of
    the book we are adding exists in the library, if it does not
    we create a new array for that genre. We then push the book
    into the array for that genre and return the library object.
    This function is now closed for modification because we can
    add new genres of book to our library without modifying this
    function. 
*/

// Look at how much simpler this function is!
// If we really want to be fancy this could all be one line. 
function addBookToLibraryRefactoredOneLine(book: Book, library: Library) {
    return library[book.genre] ? library[book.genre].push(book) : library[book.genre] = [book];
}

/* Why does it matter if a function is closed for modification?
    Well, if a function is closed for modification it means that
    we can add new functionality to our code without modifying
    existing code. This is important because it means that we
    can add new functionality to our code without breaking
    existing functionality. This is especially important in
    large code bases where it is very easy to break existing
    functionality when adding new functionality.
*/