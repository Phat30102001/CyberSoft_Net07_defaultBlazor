window.showAlert = () => alert("Hello JS")

window.showConfirm = () => {
    let res = confirm("Bạn có chắc chắc muốn xoá hay không?")
    console.log(res)

    return res
}

//  clipboard

window.copyText = async (text) =>{
   await navigator.clipboard.writeText(text)
}

// change theme, doi css cua body theem darrk-mode neeus baj theme toi

window.setDarkMode = (enable) =>{
    document.querySelector("body").classList.toggle("dark-mode",enable)
    localStorage.setItem("dark-mode",enable)
}
// hafm lay dark-mode ra kiem tra va set lai UI cho dung
window.getDarkMode =()=>{
    return localStorage.getItem('dark-mode') === "true"
}
