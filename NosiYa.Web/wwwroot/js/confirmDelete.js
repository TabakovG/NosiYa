document.addEventListener('DOMContentLoaded', function () {
    var deleteButtons = document.querySelectorAll('.delete-button');

	deleteButtons.forEach(function (form) {
        form.addEventListener('submit', function (event) {

            var confirmationValue = prompt("To confirm deletion, type 'delete':");

            if (confirmationValue !== 'delete') {
                event.preventDefault();
            }
        });
    });
});
