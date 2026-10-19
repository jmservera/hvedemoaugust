const cartFeedback = document.querySelector("[data-cart-feedback]");
if (cartFeedback) {
    const target = document.getElementById(cartFeedback.dataset.focusTarget);
    const focusTarget = target && !target.disabled ? target : cartFeedback;
    focusTarget.focus();
}
