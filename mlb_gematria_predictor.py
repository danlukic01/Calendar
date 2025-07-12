import urllib.request
import json
import datetime


def gematria_value(s: str) -> int:
    s = s.upper()
    total = 0
    for ch in s:
        if 'A' <= ch <= 'Z':
            total += ord(ch) - 64
        elif '0' <= ch <= '9':
            total += ord(ch) - 48
    return total


def digital_root(n: int) -> int:
    while n > 9:
        n = sum(int(d) for d in str(n))
    return n


def create_phrases(team: str, date: str):
    return [
        f"{team} win the game on Gregorian date {date}",
        f"{team} wins the game on Gregorian date {date}",
        f"{team} won the game on Gregorian date {date}",
        f"{team} lose the game on Gregorian date {date}",
        f"{team} loses the game on Gregorian date {date}",
        f"{team} lost the game on Gregorian date {date}"
    ]


def team_points(team: str, date: str) -> int:
    phrases = create_phrases(team, date)
    points = 0
    for idx, p in enumerate(phrases):
        val = gematria_value(p)
        root = digital_root(val)
        category = 'win' if idx < 3 else 'lose'
        if category == 'win':
            if root == 4:
                points += 1
            elif root == 2:
                points -= 1
        else:
            if root == 4:
                points -= 1
            elif root == 2:
                points += 1
    return points


def predict_winner(home: str, away: str, date: str) -> str:
    points_home = team_points(home, date)
    points_away = team_points(away, date)
    if points_home > points_away:
        return home
    elif points_away > points_home:
        return away
    else:
        return 'Draw'


def fetch_todays_games(date: str):
    url = f"https://statsapi.mlb.com/api/v1/schedule?sportId=1&date={date}"
    with urllib.request.urlopen(url) as response:
        data = json.load(response)
    games = []
    for date_block in data.get('dates', []):
        for g in date_block.get('games', []):
            home = g['teams']['home']['team']['name']
            away = g['teams']['away']['team']['name']
            game_date = g['gameDate']
            games.append({'home': home, 'away': away, 'gameDate': game_date})
    return games


def format_date(iso_date: str) -> str:
    dt = datetime.datetime.fromisoformat(iso_date.replace('Z', '+00:00'))
    return dt.strftime('%d/%m/%Y')


def main():
    today = datetime.date.today().strftime('%Y-%m-%d')
    games = fetch_todays_games(today)
    if not games:
        print('No games found for', today)
        return
    for g in games:
        date_formatted = format_date(g['gameDate'])
        winner = predict_winner(g['home'], g['away'], date_formatted)
        print(f"{g['away']} at {g['home']} on {date_formatted} -> Predicted Winner: {winner}")


if __name__ == '__main__':
    main()
