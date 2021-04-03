FROM nginx:alpine AS ng
WORKDIR /app
EXPOSE 5002
COPY "/published/wwwroot" . 
COPY "nginx.conf" /etc/nginx/nginx.conf