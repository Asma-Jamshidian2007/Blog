document.addEventListener('DOMContentLoaded', function () {
    const passwordInput = document.getElementById('password');
    const capsLockWarning = document.getElementById('capsLockWarning');

    passwordInput.addEventListener('keyup', function (event) {
        const isCapsLockOn = event.getModifierState && event.getModifierState('CapsLock');
        if (isCapsLockOn) {
            capsLockWarning.style.display = 'block'; 
        } else {
            capsLockWarning.style.display = 'none'; 
        }
    });
});