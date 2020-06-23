$(document).ready(function () {
    $('.mdb-select').formSelect();

    $('.multiple-select-dropdown').attr('name', 'SelectedUsers');
});

document.querySelector('.js-open-form').addEventListener('click', function (e) {
    if (e.target.value === 'Add to Group') {
        document.querySelector('.hidden-form').style.display = 'block';
        e.target.classList.replace('btn-success', 'btn-danger');
        e.target.value = 'Close';
    } else {
        document.querySelector('.hidden-form').style.display = 'none';
        e.target.classList.replace('btn-danger', 'btn-success');
        e.target.value = 'Add to Group';
    }
});