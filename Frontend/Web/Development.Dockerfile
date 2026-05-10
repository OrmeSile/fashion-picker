FROM node:26-alpine3.22 AS builder

WORKDIR /app

COPY package.json package-lock.json* ./

RUN --mount=type=cache,target=/root/.npm npm i

COPY . .

RUN npm run build

FROM nginx AS runner

COPY nginx.development.conf /etc/nginx/nginx.conf

COPY --chown=nginx:nginx --from=builder /app/dist/*/browser /usr/share/nginx/html
COPY /certs/localhost.crt /etc/nginx/ssl/nginx.crt
COPY /certs/localhost.key /etc/nginx/ssl/nginx.key

USER nginx

EXPOSE 8080 8443

ENTRYPOINT ["nginx", "-c", "/etc/nginx/nginx.conf"]
CMD ["-g", "daemon off;"]
