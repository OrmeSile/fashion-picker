FROM nginx AS runner

COPY nginx.development.conf /etc/nginx/nginx.conf

COPY /certs/localhost.crt /etc/nginx/ssl/nginx.crt
COPY /certs/localhost.key /etc/nginx/ssl/nginx.key

USER nginx

EXPOSE 8080 8443

ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]
