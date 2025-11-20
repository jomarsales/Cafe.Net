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
            logo.src = logoScrolled;
        }
    }

    window.addEventListener('scroll', function () {
        if (isMobileWidth()) {
            logo.src = logoScrolled;
            return;
        }

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

    setLogo();
    window.addEventListener('resize', setLogo);
});

setTimeout(function () {
    var alertEl = document.getElementById('topAlert');
    if (alertEl) {
        var bsAlert = bootstrap.Alert.getOrCreateInstance(alertEl);
        bsAlert.close();
    }
}, 5000);

$(function () {
    function updateIcons() {
        $("input, textarea").each(function () {
            var id = $(this).attr("id");
            var icon = $("#icon-" + id);

            $(this).valid();

            var error = $("#error-" + id).text().trim();

            if (!error) {
                icon.removeClass("fa-meh text-secondary fa-times text-danger").addClass("fa-check text-success");
            } else {
                icon.removeClass("fa-meh text-secondary fa-check text-success").addClass("fa-times text-danger");
            }
        });
    }

    $("input, textarea").each(function () {
        var id = $(this).attr("id");
        $("#icon-" + id).removeClass("fa-check fa-times text-success text-danger").addClass("fa-meh text-secondary");
    });

    $("form").on("submit", function () { setTimeout(updateIcons, 10); });
    $("input, textarea").on("input blur keyup", updateIcons); 
});

function initUnobtrusiveValidation(container) {
    if ($.validator && $.validator.unobtrusive) {
        $.validator.unobtrusive.parse(container);
    }
}
