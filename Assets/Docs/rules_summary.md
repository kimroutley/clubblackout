# Club Blackout — Rules Summary (consolidated)

Source: `Assets/Club Blackout Booklet.pdf`, `Assets/Club Blackout Player Booklet A5.pdf`, `Assets/Club Blackout Game Tracker.pdf` and user-provided rules.

## High-level
- Social deduction pass-and-play game. Host moderates night/day cycles.
- Roles are dealt randomly; hidden to other players.
- Night: roles perform secret actions. Day: discussion and vote to eliminate a suspect.
- Win: Party Animals win when all Dealers eliminated; Dealers win when parity or they reach a dominating number.

## Included roles (MVP subset implemented and additional notes)
- Dealer: Night killer(s). Coordinate to kill a target.
- Party Animal: Innocent baseline role.
- Medic: Choose at game start between:
  - RECUS: one instant single-use revive (immediate when someone dies), or
  - PROTECT: nightly protect that blocks a kill that night.
- Bouncer: Night ID check (Dealer or Party Animal). Can lose ability on failure.
- Tea-Spiller: On death, exposes alignment of one player.
- Wallflower: Can watch murders and hint during day without explicitly revealing what they saw.

Additional roles and details are described in full booklets (see `Assets/Club Blackout Booklet.pdf`).

## Clarifications made for implementation
- Night action order will be deterministic: Sober → Roofi → MedicProtect → BouncerCheck → ManagerInspect → DealersChoose → ResolveKills → DeathTriggers.
- Seasoned Drinker immunities and Ally Cat lives are per-night attempts; multiple dealers in one night count as a single attempt.
- Second Wind conversion happens at dealers' choose-kill step: if converted, they revive as Dealer and no kills occur that night.
- Minor is invulnerable to kills until Bouncer has ID'd them (ID is permanent).

## Art & assets
- Role portrait files found in `Assets/` (renamed to `role_<name>.png`).
- Background and player guide PDFs are available in `Assets/` and will be surfaced to hosts in the Host Tools UI.

## Next steps
- Run OCR on PDFs to extract any rule text the summary missed (confirm if you'd like me to auto-run OCR). 
- Implement in-game display or host reference view for the PDF player guides. 
- Continue building the pass-and-play UI and integrate role-specific nightly actions.

---

If you'd like I can extract full textual content from the PDFs via OCR now and append a detailed, line-by-line rule spec and test-cases.