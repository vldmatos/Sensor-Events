FROM node:alpine
WORKDIR /src
COPY ["applications/report/package*.json", "applications/report/"]
WORKDIR "/src/applications/report"
RUN npm install
COPY . .
EXPOSE 2000
CMD ["yarn", "start"]