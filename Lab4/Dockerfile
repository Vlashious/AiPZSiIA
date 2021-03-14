FROM nginx:alpine
EXPOSE 80
COPY Server/bin/Release/net5.0/publish/wwwroot /usr/share/nginx/html
COPY nginx.conf etc/nginx/nginx.conf