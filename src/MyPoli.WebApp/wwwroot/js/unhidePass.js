const togglePassword = document.querySelector('#toggleOldPassword');
const toggleNewPassword = document.querySelector('#toggleNewPassword');
const toggleConfirmPassword = document.querySelector('#toggleConfirmPassword');

const password = document.querySelector('#oldPassword');
const newpassword = document.querySelector('#newpassword');
const confirmpassword = document.querySelector('#confirmpassword');

togglePassword.addEventListener('click', function (e) {
    // toggle the type attribute
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    // toggle the eye / eye slash icon
    this.classList.toggle('bi-eye');
});
toggleNewPassword.addEventListener('click', function (e) {
    // toggle the type attribute
    const type = newpassword.getAttribute('type') === 'password' ? 'text' : 'password';
    newpassword.setAttribute('type', type);
    // toggle the eye / eye slash icon
    this.classList.toggle('bi-eye');
});
toggleConfirmPassword.addEventListener('click', function (e) {
    // toggle the type attribute
    const type = confirmpassword.getAttribute('type') === 'password' ? 'text' : 'password';
    confirmpassword.setAttribute('type', type);
    // toggle the eye / eye slash icon
    this.classList.toggle('bi-eye');
});