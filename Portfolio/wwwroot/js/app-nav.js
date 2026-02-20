window.appNav = (function () {
    function canGoBack() {
        try {
            // API de Navigation 
            if (typeof navigation !== "undefined" && navigation.entries) {
                const entries = navigation.entries();
                return entries && entries.length > 1;
            }
        } catch { /* ignorar */ }

        //Tem historio no historico do navegador e veio de outra pagina
        return window.history.length > 1 && document.referrer !== "";
    }

    function goBack() { window.history.back(); }

    return { canGoBack, goBack };
})();
