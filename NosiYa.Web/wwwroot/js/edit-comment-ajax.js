// Load the edit form for the clicked comment through AJAX and open a modal
function loadEditFormAndOpenModal(commentId) {
    fetch("/Comment/Edit/" + commentId)
        .then(response => {
            if (!response.ok) {
                console.log('Comment edit action not reachable. Response code:' + response.status);
                return;
            }
            return response.text();
        })
        .then(modalContent => {
            // Create a Bootstrap modal
            var modal = new bootstrap.Modal(document.getElementById('editCommentModal'));
            var modalBody = document.getElementById('editCommentModalBody');
            modalBody.innerHTML = modalContent;

            // Show the modal
            modal.show();
        })
        .catch(error => {
            console.log('Error:', error);
        });
}

// Add a single click event listener at the container level
var container = document.querySelector(".all-comments-container");
container.addEventListener("click", function (e) {
    var target = e.target;

    // Check if the clicked element is an "Edit Comment" link
    if (target.classList.contains("edit-comment-link")) {
        e.preventDefault();
        var commentId = target.getAttribute("data-comment-id");
        loadEditFormAndOpenModal(commentId);
    }
});
