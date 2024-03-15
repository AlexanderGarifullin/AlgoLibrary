document.addEventListener("DOMContentLoaded", function () {
    var polyfillScript = document.createElement("script");
    polyfillScript.src = "https://polyfill.io/v3/polyfill.min.js?features=es6";
    document.head.appendChild(polyfillScript);

    var mathJaxScript = document.createElement("script");
    mathJaxScript.id = "MathJax-script";
    mathJaxScript.async = true;
    mathJaxScript.src = "https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js";
    document.head.appendChild(mathJaxScript);
});
