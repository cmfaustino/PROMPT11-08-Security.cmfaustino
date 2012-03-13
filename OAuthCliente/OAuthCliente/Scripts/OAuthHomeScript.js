var token_obtido = false;

var fragmento = location.hash.substring(1);

var prim_passagem = (fragmento.length <= 0);

if (!token_obtido) {
    if(prim_passagem) {
        var u_r_l = "https://accounts.google.com/o/oauth2/auth?" +
            "response_type=" + "token" +
                "&" + "client_id=" + "1049245660536.apps.googleusercontent.com" +
    //                "&" + "redirect_uri=" + encodeURI("http://localhost/oauth2callback") +
                    "&" + "redirect_uri=" + encodeURI("http://localhost/Home/TasksImplicitExample/") +
                        "&" + "scope=" + encodeURI("https://www.googleapis.com/auth/tasks.readonly") +
                            "&" + "state=" + "%2Fprofile" +
                                "&" + "approval_prompt=" + "force";
        window.location = u_r_l;
    }

    parse_and_send();
}
