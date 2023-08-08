document.addEventListener('DOMContentLoaded', function () {
    var tabContent = document.querySelector('.alert-confirm-delete');

    tabContent.addEventListener('click', function (event) {
        // Check if the clicked element is a delete button within a form with the delete-item-form class
        var target = event.target;
        var parentForm = target.parentElement;

        if (target.classList.contains('btn-delete') && parentForm.classList.contains('delete-item-form')) {
            var confirmed = confirm('Сигурни ли сте че искате да откажете / изтриете елемента?');
            if (!confirmed) {
                event.preventDefault(); // Prevent the form submission
            }
        }
    });
});