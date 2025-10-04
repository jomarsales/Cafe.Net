window.addEventListener('DOMContentLoaded', () => {
    let scrollPos = 0;
    const mainNav = document.getElementById('mainNav');
    const headerHeight = mainNav.clientHeight;

    const logo = mainNav.querySelector("a.navbar-brand img");

    const logoDefault = "/img/svg/logo-yellow-white.svg";
    const logoScrolled = "/img/svg/logo-yellow-black.svg";

    function isMobileWidth() {
        return window.innerWidth <= 991;
    }

    function setLogo() {
        if (isMobileWidth()) {
            // no mobile sempre usa a scrolled
            logo.src = logoScrolled;
        }
    }

    window.addEventListener('scroll', function () {
        if (isMobileWidth()) {
            // mobile ignora scroll
            logo.src = logoScrolled;
            return;
        }

        // comportamento desktop
        const currentTop = document.body.getBoundingClientRect().top * -1;

        if (currentTop < scrollPos) {
            if (currentTop > 0 && mainNav.classList.contains('is-fixed')) {
                mainNav.classList.add('is-visible');
            } else {
                mainNav.classList.remove('is-visible', 'is-fixed');
                logo.src = logoDefault;
            }
        } else {
            if (currentTop > headerHeight) {
                mainNav.classList.add('is-fixed', 'is-visible');
                logo.src = logoScrolled;
            } else {
                mainNav.classList.remove('is-fixed', 'is-visible');
                logo.src = logoDefault;
            }
        }

        scrollPos = currentTop;
    });

    // aplica logo certa ao carregar e redimensionar
    setLogo();
    window.addEventListener('resize', setLogo);
});
