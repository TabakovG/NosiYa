document.addEventListener('DOMContentLoaded', function () {
    var tabContent = document.querySelector('.tab-content');

    tabContent.addEventListener('click', function (event) {
        // Check if the clicked element is a delete button within a form with the delete-item-form class
        var deleteButton = event.target;
        var parentForm = deleteButton.parentElement;

        if (deleteButton.classList.contains('btn-delete') && parentForm.classList.contains('delete-item-form')) {
            var confirmed = confirm('Are you sure you want to delete this item?');
            if (!confirmed) {
                event.preventDefault(); // Prevent the form submission
            }
        }
    });
});