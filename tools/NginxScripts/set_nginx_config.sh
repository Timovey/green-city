#!/bin/bash
rm /etc/nginx/conf.d/default.conf
echo "\
server {\n\
    listen       80;\n\
    listen  [::]:80;\n\
    server_name  localhost;\n\
\n\
    #access_log  /var/log/nginx/host.access.log  main;\n\
\n\
    location / {\n\
        root   /usr/share/nginx/html;\n\
        index  index.html index.htm;\n\
        try_files \$uri /index.html;\n\
    }\n\
\n\
    location /api {\n\
        proxy_set_header X-Real-IP \$remote_addr;\n\
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;\n\
        proxy_set_header X-NginX-Proxy true;\n\
        proxy_pass http://external-api:80;\n\
        proxy_ssl_session_reuse off;\n\
        proxy_set_header Host \$http_host;\n\
        proxy_cache_bypass \$http_upgrade;\n\
        proxy_redirect off;\n\
    }\n\
\n\
    #error_page  404              /404.html;\n\
\n\
    # redirect server error pages to the static page /50x.html\n\
    #\n\
    error_page   500 502 503 504  /50x.html;\n\
    location = /50x.html {\n\
        root   /usr/share/nginx/html;\n\
    }\n\
\n\
    # proxy the PHP scripts to Apache listening on 127.0.0.1:80\n\
    #\n\
    #location ~ \.php\$ {\n\
    #    proxy_pass   http://127.0.0.1;\n\
    #}\n\
\n\
    # pass the PHP scripts to FastCGI server listening on 127.0.0.1:9000\n\
    #\n\
    #location ~ \.php\$ {\n\
    #    root           html;\n\
    #    fastcgi_pass   127.0.0.1:9000;\n\
    #    fastcgi_index  index.php;\n\
    #    fastcgi_param  SCRIPT_FILENAME  /scripts\$fastcgi_script_name;\n\
    #    include        fastcgi_params;\n\
    #}\n\
\n\
    # deny access to .htaccess files, if Apache's document root\n\
    # concurs with nginx's one\n\
    #\n\
    #location ~ /\.ht {\n\
    #    deny  all;\n\
    #}\n\
}" >> /etc/nginx/conf.d/default.conf
nginx -s reload