﻿
function copyToClipboard(text) {
    if (window.clipboardData && window.clipboardData.setData) {
       
        return window.clipboardData.setData("Text", text);

    }
    else if (document.queryCommandSupported && document.queryCommandSupported("copy")) {
        var textarea = document.createElement("textarea");
        textarea.textContent = text;
        textarea.style.position = "fixed";  
        document.body.appendChild(textarea);
        textarea.select();
        try {
            return document.execCommand("copy");  
        }
        catch (ex) {
            console.warn("Copy to clipboard failed.", ex);
            return prompt("Copy to clipboard: Ctrl+C, Enter", text);
        }
        finally {
            document.body.removeChild(textarea);
        }
    }
}